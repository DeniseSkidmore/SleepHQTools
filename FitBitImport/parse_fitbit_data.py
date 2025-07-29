from dataclasses import dataclass
import json
import csv
from datetime import datetime
from pathlib import Path

# TODO get the data from the Fitbit API instead of a local file.
# https://python-fitbit.readthedocs.io/en/latest/

# Input and output file paths
input_file = 'input.json'

def read_input_file(file_path):
    """Read the input file and return its content.
    
    We were provided with a JSON file, but real data source should be REST API. 
    
    TODO: Replace this with actual REST API call to fetch data.
    """
    with open(file_path, 'r') as file:
        return json.load(file)
    
@dataclass
class GenericFitnessRecord:
    """Generic data source class for holding fitness data entries."""
    datetime: datetime
    value: int


def fitbit_data_to_generic(data):
    """Convert Fitbit data to generic SPO2 data format."""
    generic_data = []
    try:
        for index, entry in enumerate(data[0]['minutes']):
            generic_data.append(GenericFitnessRecord(datetime.fromisoformat(entry['minute']), int(entry['value'])))
    except KeyError as e:
        print(f"Unexpected data format: missing key {e} from item {index}: {entry}")
    except ValueError as e:
        print(f"Error parsing date or value: {e} in item {index}: {entry}")
    return generic_data

def write_output_file(output_dir: Path, spo2_data: list[GenericFitnessRecord], hr_data: list[GenericFitnessRecord], motion_data: list[GenericFitnessRecord]):
    """Write the data to a CSV file.
    TODO accept multiple data sources and merge them into one CSV file.
    """
    # TODO: group data by date and output one file per day
    min_time = min(entry.datetime for entry in spo2_data)
    start_date = min_time.strftime('%Y%m%d%H%M%S')
    file_path = output_dir/f'O2Ring_{start_date}.csv'

    # open CSV file, allow csv.DictWriter to handle the newline characters
    with open(file_path, 'w', newline='') as csvfile:
        # full file from web source Time(HH:MM:SS AM),Oxygen Level(int),Pulse Rate(int),Motion (int),Oxygen Level Reminder (int),PR Reminder(int)
        # abbreviated file from Android ViHealth App Time(HH:MM:SS AM),Oxygen Level(int),Pulse Rate(int), Motion (int)
        fieldnames = ['Time', 'Oxygen Level', 'Pulse Rate', 'Motion']
        writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
        writer.writeheader()
        
        # TODO add an option to select which data source controls the timestamps
        for entry in spo2_data:
            # TODO get the HR and Motion data from the respective lists (if available)
            writer.writerow({
                # time format from Android ViHealth App: 00:56:46 25/07/2025
                'Time': entry.datetime.strftime('%H:%M:%S %d/%m/%Y'),
                'Oxygen Level': entry.value,
                'Pulse Rate': 0,
                'Motion': 0
            })

def main():
    """Main function to convert from fitbit input to O2Ring output."""
    print("Starting Fitbit data transformation...")
    # TODO prompt the user for the input file path
    data = read_input_file(input_file)
    write_output_file(Path.home(), fitbit_data_to_generic(data), [], [])

if __name__ == "__main__":
    main()