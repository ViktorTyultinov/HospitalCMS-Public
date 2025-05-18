output "cluster_name" {
  value = google_container_cluster.gke.name
}

output "endpoint" {
  value = google_container_cluster.gke.endpoint
}

output "location" {
  value = google_container_cluster.gke.location
}
