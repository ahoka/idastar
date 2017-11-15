pipeline {
  agent {
    docker {
      image 'microsoft/dotnet:2.0-sdk'
      args '-v /.dotnet:/root/.dotnet'
      customWorkspace "/b/${BRANCH_NAME}"
    }
    
  }
  stages {
    stage('Build') {
      steps {
        sh 'dotnet build --no-cache'
      }
    }
    stage('Test') {
      steps {
        sh 'dotnet test'
      }
    }
  }
  environment {
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
    HOME = 'home'
  }
}
