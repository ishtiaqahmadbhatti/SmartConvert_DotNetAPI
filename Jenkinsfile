@Library("Shared") _
pipeline {
    
    agent {label 'Ishtiaq'}

    stages {
        stage('Hello') {
            steps {
                script{
                    hello()
                }
            }
        }
        stage('Clone Code') {
            steps {
                script{
                    cloneCode("https://github.com/ishtiaqahmadbhatti/SmartConvert_DotNetAPI.git", "main")
                }
            }
        }
        stage('Build Code') {
            steps {
                script{
                    buildCode("smartconvert-dotnetapi", "latest", "ishtiaqahmad913")
                }
            }
        }
        stage("Push To DockerHub"){
            steps{
                script {
                    pushToDockerHub(
                        "dockerHubCred",
                        "dockerHubUser",
                        "dockerHubPass",
                        "smartconvert-dotnetapi"
                    )
                }
            }
        }
         stage('Deploy Code') {
            steps {
                sh "docker compose down && docker compose down -d"
            }
        }
    }
}
