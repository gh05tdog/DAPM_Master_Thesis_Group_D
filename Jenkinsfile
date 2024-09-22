pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://your-repository-url.git'
            }
        }

        stage('Navigate to DAPM Directory and Stop Existing Containers') {
            steps {
                dir('DAPM') {
                    script {
                        sh 'docker-compose down || true'
                    }
                }
            }
        }

        stage('Build and Run with Docker Compose') {
            steps {
                dir('DAPM') {
                    script {
                        sh 'docker-compose up --build -d'
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
