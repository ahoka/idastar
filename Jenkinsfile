pipeline {
  agent {
    docker {
      image 'microsoft/dotnet:2.0-sdk'
      args '-v /.dotnet:/root/.dotnet'
    }
    
  }
  stages {
    stage('Build') {
      steps {
        sh 'pwd'
        sh 'ls -la'
        sh 'id'
      }
    }
  }
  environment {
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
  }
}