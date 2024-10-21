#!/bin/sh

# Start Ollama server in the background
ollama serve &

# Capture the PID of the Ollama server process
OLLAMA_PID=$!

# Wait for the server to be ready (adjust as needed)
sleep 5

# Pull the model while the server is running
ollama run qwen2.5:0.5b

# Wait for the Ollama server process to keep the container running
wait $OLLAMA_PID
