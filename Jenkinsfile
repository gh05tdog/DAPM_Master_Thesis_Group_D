pipeline {
    agent any

    environment {
        ASPNETCORE_ENVIRONMENT = 'Jenkins'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/gh05tdog/DAPM_Master_Thesis_Group_D.git'
            }
        }

        stage('Navigate to DAPM Directory and Stop Existing Containers') {
            steps {
                dir('DAPM') {
                    script {
                        sh 'docker compose down || true'
                    }
                }
            }
        }

        stage('Navigate to DAPM-Frontend Directory and Stop Existing Containers') {
            steps {
                dir('DAPM-Frontend') {
                    script {
                        sh 'docker compose down || true'
                    }
                }
            }
        }

        stage('Build and Run backend with Docker Compose') {
            steps {
                dir('DAPM') {
                    script {
                        sh 'docker compose up --build -d'
                    }
                }
            }
        }

        stage('Build and Run frontend with Docker Compose') {
            steps {
                dir('DAPM-Frontend') {
                    script {
                        sh 'docker compose up --build -d'
                    }
                }
            }
        }

        stage('Run end to end tests') {
            steps {
                dir('DAPM') {
                    script {
                        sh 'docker compose run --rm dapm.clientapiendtoendtest'
                    }
                }
            }
        }
    }

    post {
        failure {
            script {
                echo 'Pipeline failed. Please check the logs.'
            }
        }
    }
}
