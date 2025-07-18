def GetImageVersion() {
    def currentDateTime = new Date().format("yyyyMMddHHmmss")
    return currentDateTime
}
def GetAgent()
{
    if( env.BRANCH_NAME =='staging')
    {
        return 'acl-eap-csu-dev'
    }
    else if(env.BRANCH_NAME == 'production')
    {
        return 'acl-eap-csu'
    }
    return 'acl-eap-csu-dev'
}

def imageName
def latestName
def buildEnv
def branchName = env.BRANCH_NAME
def composeEnv

pipeline {
  agent {
    kubernetes {
	   label 'dynamic-agent'
	}
  }
	environment {
        HARBOR_HOSTNAME="https://eap-cr.advantech.com.tw"
        HARBOR_CREDENTIALS_ID = 'EAP.CICDHarbor'
        HARBOR_REPOSITORY = 'eap-cr.advantech.com.tw/productservice/web'
        EMAIL_NOTIDICATION_LIST='IT-Developers@advantechO365.onmicrosoft.com'
        HARBOR_PASSWORD="Adv2024cicd"
    }
    stages {
	
        stage('git checkout - k8s') {
            steps {
                script {
                    git branch: branchName,
					credentialsId: 'test',
					url: 'https://github.com/WillLin-Advantech/Test.git'
                }
            }
        }
		
		stage('build') {
            steps {
			    script {
					if (branchName == 'staging')
                    {
                        imageName = "${env.HARBOR_REPOSITORY}:stage-${GetImageVersion()}-t"
                        latestName="${env.HARBOR_REPOSITORY}:latest-t"
                        buildEnv='Debug'
                        composeEnv='Stage'
                    }
                    else if(branchName == 'production')
                    {
                        imageName = "${env.HARBOR_REPOSITORY}:production-${GetImageVersion()}"
                        latestName="${env.HARBOR_REPOSITORY}:latest"
                        buildEnv='Release'
                        composeEnv='Production'
                    }
					// for test
					else
					{
						imageName = "${env.HARBOR_REPOSITORY}:stage-${GetImageVersion()}-t"
                        latestName="${env.HARBOR_REPOSITORY}:latest-t"
                        buildEnv='Debug'
                        composeEnv='Stage'
					}
				}
                container('docker-client') {
                    script {
						withEnv(["DOCKER_HOST=tcp://localhost:2375"]) {
							withCredentials([usernamePassword(credentialsId: 'harborLogin', usernameVariable: 'HARBOR_USERNAME', passwordVariable: 'HARBOR_PASSWORD')]) {
								sh """
									docker build -t ${imageName} -f src/HelloWorld.HttpApi.Host/Dockerfile --build-arg BUILD_CONFIGURATION=${buildEnv} .
									docker build -t ${latestName} -f src/HelloWorld.HttpApi.Host/Dockerfile --build-arg BUILD_CONFIGURATION=${buildEnv} .
									echo "$HARBOR_PASSWORD" | docker login "$HARBOR_HOSTNAME" -u "$HARBOR_USERNAME" --password-stdin
									docker push ${imageName}
									docker push ${latestName}
								"""
							}
						}
                        
                    }
                }
            }
        }
		

        stage('Deploy to K8s') {
            steps {
                container('kubectl') {
					script {
						sh """
							kubectl delete -f ${WORKSPACE}/service.yaml
							kubectl delete -f ${WORKSPACE}/deployment.yaml
							sed -i -e "s|REPLACE_IMAGE|${latestName}|g" -e "s|REPLACE_ENV|${composeEnv}|g" ${WORKSPACE}/deployment.yaml
							kubectl apply -f ${WORKSPACE}/service.yaml
							kubectl apply -f ${WORKSPACE}/deployment.yaml
						"""
					}
                }
            }
        }
		

    }
}
