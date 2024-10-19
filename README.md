# The Force

## Running the Project with Ollama API Key

To run the project locally with an API key for the Ollama service, you'll need to set the `OLLAMA_API_KEY` environment variable in your respective shell before starting the Docker containers. Here’s how to do it for different environments:

### PowerShell

For **PowerShell**, use the following command to set the `OLLAMA_API_KEY` and then start the Docker containers:

```powershell```
$env:OLLAMA_API_KEY = "ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIKBismC6dwcDhjmgU0q+30iUAQSwyvp6OAJTNCzOOUo5"
docker-compose up -d

### CMD (Command Prompt)

For Windows Command Prompt (CMD), use the set command to define the environment variable:

### cmd

set OLLAMA_API_KEY=ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIKBismC6dwcDhjmgU0q+30iUAQSwyvp6OAJTNCzOOUo5
docker-compose up -d

Linux/Mac (Bash)

For Linux or macOS (Bash shell), set the environment variable inline with the docker-compose command:

bash

OLLAMA_API_KEY="ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIKBismC6dwcDhjmgU0q+30iUAQSwyvp6OAJTNCzOOUo5" docker-compose up -d

Notes

    These commands temporarily set the OLLAMA_API_KEY environment variable for this session only.
    Ensure you use the correct API key for your setup, as the example key here is for demonstration purposes.
    Run docker-compose down --volumes when you're finished to clean up the containers and volumes.

### Breakdown:
- I added a brief introduction and unified the section titles under "Running the Project with Ollama API Key."
- Provided a consistent explanation for how to use the commands.
- Included a note section for clarity on key usage and cleanup.