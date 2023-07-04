# Elixir Application

This application allows users to find possible magic elixirs based on the available ingredients.

## Introduction

The Magic Elixir Application is a console-based program that helps users discover possible elixirs they can create using a set of available ingredients. The program interacts with an API to retrieve the list of ingredients and elixirs, and then performs the necessary calculations to determine the possible elixirs that can be made with the given ingredients.

## Getting Started

To get started with the Magic Elixir Application, follow these steps:

1. Clone the repository to your local machine.
2. Build the solution using your preferred IDE or build tool.

## Configuration

The application requires a configuration file named `appsettings.json` to be present in the application directory. Make sure to provide the following settings in the configuration file:

```json
{
  "BaseUrl": "https://wizard-world-api.herokuapp.com/",
  "IncludeElixirWithNoIngredients": true
}
```

- **BaseUrl**: The base URL of the API that provides the ingredients and elixirs data.
- **IncludeElixirWithNoIngredients**: A flag indicating whether to include elixirs with no ingredients in the results.

Make sure to replace `https://wizard-world-api.herokuapp.com/` with the actual base URL of your API.

## Running the Application

To run the Magic Elixir Application, follow these steps:

1. Open a command prompt or terminal.
2. Navigate to the root directory of the cloned repository.
3. Ensure that you have the necessary dependencies installed and available. If you haven't already, restore the dependencies by running the following command:

   ```bash
   dotnet restore
   ```

   This command will restore the required NuGet packages specified in the project file.

4. Build the solution by running the following command:

   ```bash
   dotnet build
   ```

   This command will compile the application and generate the executable file.

5. Navigate to the output directory containing the built executable file. Use the following command to change to the appropriate directory:

   ```bash
   cd path/to/output/directory
   ```

   Replace `path/to/output/directory` with the actual path to the output directory where the executable file is located.

6. Configure the application by creating a `appsettings.json` file in the output directory with the following content:

   ```json
   {
     "BaseUrl": "https://wizard-world-api.herokuapp.com/",
     "IncludeElixirWithNoIngredients": true
   }
   ```

   Replace `https://wizard-world-api.herokuapp.com/` with the actual base URL of your API.

7. Run the application using the following command:

   ```bash
   dotnet MagicElixirApplication.dll
   ```

   This command will execute the application.

8. The application will prompt you to enter the available ingredients. Enter one ingredient ID per line and press Enter after each entry. To finish entering ingredients, leave a blank line and press Enter.

9. The application will calculate and display the possible elixirs that can be made with the available ingredients.

Make sure to follow the instructions carefully and replace any placeholder values with the appropriate information specific to your setup.

Please note that the commands provided assume that you have the .NET Core CLI installed. If you're using a different build tool or IDE, adjust the commands accordingly.

## Approach

The Magic Elixir Application follows the following approach to find possible elixirs:

1. Retrieve the list of ingredients and elixirs from the API using the provided base URL.
2. Initialize the `MagicElixirService` with the retrieved ingredients and elixirs.
3. Prompt the user to enter the available ingredients.
4. Validate the entered ingredient IDs and add them to the list of available ingredients.
5. Find the relevant elixirs that can be made with the available ingredients.
6. Display the list of possible elixirs to the user.

## Assumptions

The Magic Elixir Application makes the following assumptions:

1. The API endpoints for retrieving ingredients and elixirs are properly configured and accessible using the provided base URL.
2. The ingredient IDs entered by the user are case-insensitive and should match the IDs retrieved from the API.
3. The application does not handle errors related to network connectivity or API failures. It assumes that the API will return the expected data.
4. The application uses a parallel loop to process the elixirs, assuming that the hardware and resources are capable of parallel execution.

Please review and update these assumptions based on your specific requirements and environment.

## Dependencies

The application relies on the following dependencies:

- Newtonsoft.Json (version 13.0.1)

Make sure these dependencies are installed and referenced in the project.