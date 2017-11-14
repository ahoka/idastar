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
      }
    }
  }
}