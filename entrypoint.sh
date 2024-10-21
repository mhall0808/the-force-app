#!/bin/sh

# Start Ollama server in the background
ollama serve &

# Capture the PID of the Ollama server process
OLLAMA_PID=$!

# Wait for the server to be ready (increased wait time)
echo "Waiting for the Ollama server to start..."
sleep 10

# Pull the model while the server is running
echo "Pulling the model qwen2.5:0.5b..."
ollama pull qwen2.5:0.5b

# Check if the model pull was successful
if [ $? -ne 0 ]; then
  echo "Failed to pull the model qwen2.5:0.5b."
  exit 1
fi

echo "Model pull successful."

# Wait for the Ollama server process to keep the container running
wait $OLLAMA_PID
