# C# AWS Lambda Project

## Overview
This project is an AWS Lambda function written in **C#**. It is designed to run serverless on AWS and can be triggered via API Gateway, S3, DynamoDB, or other AWS services.

## Features
- Developed using **.NET Core**.
- Uses AWS Lambda for serverless execution.
- Integrates with AWS services like **S3, DynamoDB, and API Gateway**.
- Optimized for scalability and cost-efficiency.

## Prerequisites
Ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download)
- [AWS CLI](https://aws.amazon.com/cli/)
- [AWS Toolkit for Visual Studio](https://aws.amazon.com/visualstudio/)
- Git

## Setup Instructions
1. **Clone the Repository:**
   ```sh
   git clone <repository-url>
   cd <project-folder>
   ```
2. **Restore Dependencies:**
   ```sh
   dotnet restore
   ```
3. **Build the Project:**
   ```sh
   dotnet build
   ```
4. **Run Locally:**
   ```sh
   dotnet run
   ```
5. **Deploy to AWS Lambda:**
   ```sh
   dotnet lambda deploy-function <function-name>
   ```

## Git Setup
Make sure `launchSettings.json` is ignored in `.gitignore`:
```
**/Properties/launchSettings.json
```

### Git Commands to Push Code
```sh
git init
git remote add origin <repo-url>
echo **/Properties/launchSettings.json > .gitignore
git add .
git commit -m "Initial commit"
git push -u origin main
```

## Testing
- Use **Postman** or **cURL** to send test requests if using API Gateway.
- Write unit tests with **xUnit or NUnit** and run:
  ```sh
  dotnet test
  ```

## License
This project is licensed under the MIT License.

