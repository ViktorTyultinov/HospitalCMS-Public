provider "google" {
  project = var.gcp_project
  region  = var.gcp_region
}

resource "google_container_cluster" "gke" {
  name     = var.cluster_name
  location = var.gcp_region

  initial_node_count = 2

  node_config {
    machine_type = "e2-medium"
  }

  remove_default_node_pool = false
}

resource "google_project_service" "container" {
  service = "container.googleapis.com"
}

resource "google_project_service" "compute" {
  service = "compute.googleapis.com"
}
