pipeline {
  agent {
    docker {
      image 'microsoft/dotnet:2.0-sdk'
      args '-v $HOME/.dotnet:/root/.dotnet'
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