pipeline {
    agent any

    environment {
        ENV_FILE = '.env.jenkins'
    }

    stages {
        stage('Replace localhost in appsettings.json to se2-d.compute.dtu.dk') {
            steps {
                script {
                    sh '''
                        find . -type f -name "appsettings.json" -exec sed -i 's/localhost/se2-d.compute.dtu.dk/g' {} +
                    '''
                }
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

        stage('Run unit tests') {
            steps {
                sh 'dotnet test ./DAPM/DAPM.AccessControlService.Test.Unit/DAPM.AccessControlService.Test.Unit.csproj'
            }
        }

        stage('Run end to end tests') {
            steps {
                sh 'dotnet test ./DAPM.Backend.Test.EndToEnd'
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
