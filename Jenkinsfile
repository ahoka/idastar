pipeline {
  agent {
    docker {
      image 'microsoft/dotnet:2.0-sdk'
    }
    
  }
  stages {
    stage('Build') {
      steps {
        sh 'dotnet build'
      }
    }
  }
}