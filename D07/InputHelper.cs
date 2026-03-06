namespace D07
{
    public static class InputHelper
    {
        public static byte ReadID(string message, byte min, byte max)
        {
            byte value = 0;
            bool isValid;

            do
            {
                Console.Write(message);

                if (!byte.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("Invalid number. Try again.");
                    isValid = false;
                }
                else if (value < min || value > max)
                {
                    Console.WriteLine($"Value must be between {min} and {max}. Try again.");
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }

            } while (!isValid);

            return value;
        }

        public static List<byte> ReadMultipleIDs(string message, byte min, byte max)
        {
            List<byte> values;
            bool valid;

            do
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                values = new List<byte>();
                valid = true;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input.");
                    valid = false;
                    continue;
                }

                var parts = input.Split(',');

                foreach (var part in parts)
                {
                    if (!byte.TryParse(part.Trim(), out byte number) ||
                        number < min || number > max)
                    {
                        valid = false;
                        break;
                    }

                    values.Add(number);
                }

                if (!valid)
                    Console.WriteLine("Invalid numbers. Try again.");

            } while (!valid);

            return values;
        }
    }
}
