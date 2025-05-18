using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO.Compression;

namespace Hospital.Infrastructure.Persistance.BackgroundServices;

public class DatabaseBackupService(ILogger<DatabaseBackupService> logger) : BackgroundService
{
    private readonly ILogger<DatabaseBackupService> _logger = logger;

    private const string BackupFolder = "/backups";
    private const string SqlFile = "backup.sql";
    private const string GzipFile = "backup.sql.gz";
    private const string DbConnection = "postgresql://username:password@localhost:5432/dbname";
    private const string RemotePath = "/remote/backups/backup.sql.gz";

    private const string SftpHost = "sftp.example.com";
    private const int SftpPort = 22;
    private const string SftpUser = "sftpuser";
    private const string SftpPassword = "sftppassword";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextMidnight = now.Date.AddDays(1);
            var delay = nextMidnight - now;

            _logger.LogInformation("Waiting until {Time} for next backup", nextMidnight);

            try
            {
                await Task.Delay(delay, stoppingToken);
                await RunBackupAsync(stoppingToken);
            }
            catch (TaskCanceledException) { break; }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in backup scheduler");
            }
        }
    }

    private async Task RunBackupAsync(CancellationToken token)
    {
        try
        {
            Directory.CreateDirectory(BackupFolder);
            var sqlPath = Path.Combine(BackupFolder, SqlFile);
            var gzPath = Path.Combine(BackupFolder, GzipFile);

            await DumpDatabaseAsync(sqlPath, token);
            Compress(sqlPath, gzPath);
            UploadToSftp(gzPath);

            File.Delete(sqlPath);
            File.Delete(gzPath);

            _logger.LogInformation("Backup process completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Backup process failed");
        }
    }

    private async Task DumpDatabaseAsync(string outputPath, CancellationToken token)
    {
        _logger.LogInformation("Starting pg_dump to {Path}", outputPath);

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "pg_dump",
                Arguments = $"--dbname={DbConnection} -F p -f \"{outputPath}\"",
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        var error = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync(token);

        if (process.ExitCode != 0)
            throw new Exception($"pg_dump failed: {error}");
    }

    private void Compress(string input, string output)
    {
        _logger.LogInformation("Compressing backup");

        using var source = File.OpenRead(input);
        using var target = File.Create(output);
        using var gzip = new GZipStream(target, CompressionLevel.Optimal);
        source.CopyTo(gzip);
    }

    private void UploadToSftp(string path)
    {
        _logger.LogInformation("Uploading backup to SFTP");

        // using var client = new SftpClient(SftpHost, SftpPort, SftpUser, SftpPassword);
        // client.Connect();

        // using var fileStream = File.OpenRead(path);
        // client.UploadFile(fileStream, RemotePath, true);

        // client.Disconnect();
    }
}
