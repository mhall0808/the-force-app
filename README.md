# Star Wars Crawl Adventure

#### Embark on a galactic journey with the Star Wars Crawl Adventure! This interactive web application combines data from the Star Wars universe with AI-generated content to create a unique and immersive experience.
## Table of Contents

Features
Technologies Used
Architecture
Prerequisites
Installation and Setup
Usage
Acknowledgments

## Features

Random Star Wars Entities: Fetch random characters, starships, planets, films, vehicles, and species from a seeded SQL database.
Interactive UI: A responsive and user-friendly interface optimized for both desktop and mobile devices.
AI-Generated Star Wars Crawl: Generate a custom Star Wars opening crawl powered by AI using Ollama.
Full CRUD Functionality: Manage Starship data through Create, Read, Update, and Delete operations.
Modern Design: Incorporates the latest Bootstrap components for a sleek and modern look.

## Technologies Used

Backend: .NET 8.0 API using ASP.NET Core MVC
Frontend: React.js with Bootstrap for styling
Database: Microsoft SQL Server with Entity Framework Core
AI Service: Ollama
Containerization: Docker and Docker Compose

## Architecture

### The project consists of four main components:

MS SQL Server: Hosts the database seeded with Star Wars data from the SWAPI API.
.NET 8.0 API: Serves as the backend, providing RESTful API endpoints for the frontend.
React Frontend: The user interface where users interact with the application.
Ollama AI Service: Generates AI-powered content for the Star Wars crawl.

#### Prerequisites

Git: To clone the repository.
Docker & Docker Compose: To build and run the containers.
At least 8GB of RAM: Recommended for running all services smoothly.

## Installation and Setup

### Follow these steps to set up and run the project on your local machine.
### 1. Clone the Repository

#### bash

```
git clone https://github.com/yourusername/star-wars-crawl-adventure.git
cd star-wars-crawl-adventure
```

### 2. Build and Run with Docker Compose

### Ensure Docker and Docker Compose are installed and running on your system.

#### bash

`docker-compose up --build`

#### This command will:

Build the .NET API and React frontend images.
Pull the necessary images for SQL Server and Ollama.
Start all services defined in the docker-compose.yml file.

### 3. Verify the Setup

Frontend: Open your browser and navigate to http://localhost:3000 to access the React application.
API: The backend API is accessible at http://localhost:5000.
Ollama: The AI service runs on http://localhost:11434.

## Usage

Access the Application: Navigate to http://localhost:3000 in your web browser.

Interact with Entities:
    Click on the icons to fetch random Star Wars entities.
    The entities include people, starships, planets, films, vehicles, and species.

Generate the Star Wars Crawl:
    After selecting your entities, click the GO button.
    An AI-generated Star Wars opening crawl will be displayed, incorporating your selected entities.

Manage Starships:
    Use the CRUD functionality to create, read, update, or delete starship data.

## Acknowledgments

SWAPI - The Star Wars API
Ollama AI Service
Bootstrap
React