// MIT License

// Copyright(c) 2026 Ashton Scott

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace AshtonScott_Animation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set console encoding to UTF8
            // Not sure if i actually need this but I don't think it hurts
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Setup the animation path as a constant variable
            const string PathAnimation = "C:\\Users\\Ashton Scott\\Visual Studio\\source\\repos\\AshtonScott_Animation\\animation";

            // Grab the file that contains the 'attention grabber' and print it
            string attention = File.ReadAllText($"{PathAnimation}\\attention.txt");
            Console.WriteLine(attention);

            Console.WriteLine("ATTENTION USER!\n");
            Console.WriteLine("BEFORE YOU PLAY THIS ANIMATION, MAKE SURE YOU FULL-SCREEN THE CONSOLE WINDOW!");
            Console.WriteLine("IF THE ANIMATION APPEARS TO BE GARBLED, EITHER YOUR CONSOLE WINDOW OR MONITOR IS TOO SMALL!\n");
            Console.Write("PRESS ENTER TO CONTINUE...");
            Console.ReadLine();

            // Save all frame paths to an array and sort them
            string[] animation = Directory.GetFiles(PathAnimation, "*_frame.txt");
            Array.Sort(animation); // This sorts all the frames in order

            // Animation loop
            while (true)
            {
                foreach (string frame in animation)
                {
                    int delay = 50; // This is the global frame delay

                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, 0); // Return cursor to orgin so we can overwrite new frames

                    // For the current frame, read all of it's lines to an array
                    string[] content = File.ReadAllLines(frame);

                    foreach (string line in content)
                    {
                        // Create a new instance of StringBuilder
                        var output = new System.Text.StringBuilder();

                        // Check to see if a frame has a local delay
                        if (line.StartsWith("DELAY="))
                        {
                            delay = Convert.ToInt32(line.Substring(6));
                            continue;
                        }

                        // Go through each letter in line to append ANSI color values
                        // https://en.wikipedia.org/wiki/ANSI_escape_code#Colors
                        foreach (char letter in line) 
                        {
                            switch (letter)
                            {
                                case '█':
                                    output.Append("\x1b[38;2;255;255;220m" + letter + "\x1b[0m");
                                    break;
                                case '▓':
                                    output.Append("\x1b[38;2;255;220;120m" + letter + "\x1b[0m");
                                    break;
                                case '▒':
                                    output.Append("\x1b[38;2;255;140;0m" + letter + "\x1b[0m");
                                    break;
                                case '░':
                                    output.Append("\x1b[38;2;180;30;0m" + letter + "\x1b[0m");
                                    break;
                                default:
                                    output.Append(letter);
                                    break;
                            }
                        }

                        // Print the frame contents with the modified characters
                        Console.WriteLine(output.ToString());
                    }

                    Thread.Sleep(delay);
                }
            }
        }
    }
}
