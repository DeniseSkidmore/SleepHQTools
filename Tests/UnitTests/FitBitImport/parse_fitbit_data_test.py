from pathlib import Path
import unittest
from FitBitImport import parse_fitbit_data


class TestParseFitbitData(unittest.TestCase):
    def test_loads_json(self):
        result = parse_fitbit_data.read_input_file(
            Path(__file__).parent/'response_1753568793328.json')
        self.assertIsInstance(result, list)

    def test_parse_fitbit_to_generic(self):
        fitbit = parse_fitbit_data.read_input_file(
            Path(__file__).parent/'response_1753568793328.json')
        result = parse_fitbit_data.fitbit_data_to_generic(fitbit)
        self.assertIsInstance(result, list)
        self.assertIsInstance(result[0], parse_fitbit_data.GenericFitnessRecord)

    def test_write_csv(self):
        fitbit = parse_fitbit_data.read_input_file(
            Path(__file__).parent/'response_1753568793328.json')
        generic = parse_fitbit_data.fitbit_data_to_generic(fitbit)
        parse_fitbit_data.write_output_file(
            Path(__file__).parent, generic, [], [])
        # manually inspect output to verify correctness


if __name__ == "__main__":
    unittest.main()