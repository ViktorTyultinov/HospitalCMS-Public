using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hospitals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    hospital_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                    table.ForeignKey(
                        name: "FK_departments_hospitals_hospital_id",
                        column: x => x.hospital_id,
                        principalTable: "hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "general_practitioner_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hospital_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_practitioner_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_general_practitioner_profiles_hospitals_hospital_id",
                        column: x => x.hospital_id,
                        principalTable: "hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "nurse_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hospital_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nurse_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_nurse_profiles_hospitals_hospital_id",
                        column: x => x.hospital_id,
                        principalTable: "hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "specialist_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    specialty = table.Column<int>(type: "integer", nullable: false),
                    hospital_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialist_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_specialist_profiles_hospitals_hospital_id",
                        column: x => x.hospital_id,
                        principalTable: "hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    room_number = table.Column<int>(type: "integer", nullable: false),
                    floor = table.Column<int>(type: "integer", nullable: false),
                    department_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_rooms_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "storage_units",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    department_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storage_units", x => x.id);
                    table.ForeignKey(
                        name: "FK_storage_units_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_devices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    serial_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    device_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    hospital_id = table.Column<Guid>(type: "uuid", nullable: false),
                    storage_unit_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_devices", x => x.id);
                    table.ForeignKey(
                        name: "FK_medical_devices_hospitals_hospital_id",
                        column: x => x.hospital_id,
                        principalTable: "hospitals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_medical_devices_storage_units_storage_unit_id",
                        column: x => x.storage_unit_id,
                        principalTable: "storage_units",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "beds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bed_number = table.Column<int>(type: "integer", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    patient_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beds", x => x.id);
                    table.ForeignKey(
                        name: "FK_beds_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diagnoses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    diagnosis_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    general_practitioner_check_up_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagnoses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "general_practitioner_check_ups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    checkup_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    diagnosis_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prescription_id = table.Column<Guid>(type: "uuid", nullable: false),
                    patient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    general_practitioner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_practitioner_check_ups", x => x.id);
                    table.ForeignKey(
                        name: "FK_general_practitioner_check_ups_general_practitioner_profile~",
                        column: x => x.general_practitioner_id,
                        principalTable: "general_practitioner_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    prescription_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    general_practitioner_check_up_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_prescriptions_general_practitioner_check_ups_general_practi~",
                        column: x => x.general_practitioner_check_up_id,
                        principalTable: "general_practitioner_check_ups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prescription_lines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    medication_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dosage = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    frequency = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    instructions = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    prescription_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescription_lines", x => x.id);
                    table.ForeignKey(
                        name: "FK_prescription_lines_prescriptions_prescription_id",
                        column: x => x.prescription_id,
                        principalTable: "prescriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_histories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_histories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patient_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    medical_history_id = table.Column<Guid>(type: "uuid", nullable: true),
                    general_practitioner_profile_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_patient_profiles_general_practitioner_profiles_general_prac~",
                        column: x => x.general_practitioner_profile_id,
                        principalTable: "general_practitioner_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_patient_profiles_medical_histories_medical_history_id",
                        column: x => x.medical_history_id,
                        principalTable: "medical_histories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    general_practitioner_profile_id = table.Column<Guid>(type: "uuid", nullable: true),
                    specialist_profile_id = table.Column<Guid>(type: "uuid", nullable: true),
                    nurse_profile_id = table.Column<Guid>(type: "uuid", nullable: true),
                    patient_profile_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_general_practitioner_profiles_general_practitioner_pr~",
                        column: x => x.general_practitioner_profile_id,
                        principalTable: "general_practitioner_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_nurse_profiles_nurse_profile_id",
                        column: x => x.nurse_profile_id,
                        principalTable: "nurse_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_patient_profiles_patient_profile_id",
                        column: x => x.patient_profile_id,
                        principalTable: "patient_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_specialist_profiles_specialist_profile_id",
                        column: x => x.specialist_profile_id,
                        principalTable: "specialist_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_beds_patient_id",
                table: "beds",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_beds_room_id_bed_number",
                table: "beds",
                columns: new[] { "room_id", "bed_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_departments_hospital_id",
                table: "departments",
                column: "hospital_id");

            migrationBuilder.CreateIndex(
                name: "IX_diagnoses_general_practitioner_check_up_id",
                table: "diagnoses",
                column: "general_practitioner_check_up_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_general_practitioner_check_ups_general_practitioner_id",
                table: "general_practitioner_check_ups",
                column: "general_practitioner_id");

            migrationBuilder.CreateIndex(
                name: "IX_general_practitioner_check_ups_patient_id",
                table: "general_practitioner_check_ups",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_general_practitioner_profiles_hospital_id",
                table: "general_practitioner_profiles",
                column: "hospital_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_devices_hospital_id",
                table: "medical_devices",
                column: "hospital_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_devices_storage_unit_id",
                table: "medical_devices",
                column: "storage_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_histories_PatientId",
                table: "medical_histories",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_nurse_profiles_hospital_id",
                table: "nurse_profiles",
                column: "hospital_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_profiles_general_practitioner_profile_id",
                table: "patient_profiles",
                column: "general_practitioner_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_profiles_medical_history_id",
                table: "patient_profiles",
                column: "medical_history_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_prescription_lines_prescription_id",
                table: "prescription_lines",
                column: "prescription_id");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_general_practitioner_check_up_id",
                table: "prescriptions",
                column: "general_practitioner_check_up_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rooms_department_id",
                table: "rooms",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_specialist_profiles_hospital_id",
                table: "specialist_profiles",
                column: "hospital_id");

            migrationBuilder.CreateIndex(
                name: "IX_storage_units_department_id",
                table: "storage_units",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_general_practitioner_profile_id",
                table: "users",
                column: "general_practitioner_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_nurse_profile_id",
                table: "users",
                column: "nurse_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_patient_profile_id",
                table: "users",
                column: "patient_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_specialist_profile_id",
                table: "users",
                column: "specialist_profile_id");

            migrationBuilder.AddForeignKey(
                name: "FK_beds_patient_profiles_patient_id",
                table: "beds",
                column: "patient_id",
                principalTable: "patient_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_diagnoses_general_practitioner_check_ups_general_practition~",
                table: "diagnoses",
                column: "general_practitioner_check_up_id",
                principalTable: "general_practitioner_check_ups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_general_practitioner_check_ups_patient_profiles_patient_id",
                table: "general_practitioner_check_ups",
                column: "patient_id",
                principalTable: "patient_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_medical_histories_patient_profiles_PatientId",
                table: "medical_histories",
                column: "PatientId",
                principalTable: "patient_profiles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medical_histories_patient_profiles_PatientId",
                table: "medical_histories");

            migrationBuilder.DropTable(
                name: "beds");

            migrationBuilder.DropTable(
                name: "diagnoses");

            migrationBuilder.DropTable(
                name: "medical_devices");

            migrationBuilder.DropTable(
                name: "prescription_lines");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "storage_units");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "nurse_profiles");

            migrationBuilder.DropTable(
                name: "specialist_profiles");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "general_practitioner_check_ups");

            migrationBuilder.DropTable(
                name: "patient_profiles");

            migrationBuilder.DropTable(
                name: "general_practitioner_profiles");

            migrationBuilder.DropTable(
                name: "medical_histories");

            migrationBuilder.DropTable(
                name: "hospitals");
        }
    }
}
