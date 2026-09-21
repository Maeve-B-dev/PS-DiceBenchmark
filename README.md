# Lex Magia - DiceBenchmark
This is a small command-line tool, designed to benchmark and balance Lex Magias dice system.

## Installation
Go to [Releases](https://github.com/Maeve-B-dev/PS-DiceBenchmark/releases) and download the latest version.

### Windows
Execute the programm normally. 
To use command line arguments, open a terminal window in your installation folder and enter the name of your binary preceeded by a `.\`, for example `.\DiceBenchmarkV0.2_win64`.
For available Arguments, see [Usage](#usage)

### Linux
Once the download has finished, move the binary to where you want to install the programm and start a terminal window there.
Use `sudo chmod +x [filename]` to make the file executable and run it using `./[filename]`. Replace [filename] with the name of your downloaded binary.
For available Arguments, see [Usage](#usage)

## Usage
This program supports multiple command line arguments.

`-v`: Enables verbose mode. **Caution! This will output each individual roll and can severly impact performance.**

`--tw [number]`, `--bw [number]`, `--mw [number]`, `--sw [number]`: Used to set the amount of dice for each category. The number must be an int >= 0.

`--rolls [number]`: Set the amount of rolls to be performed. The number must be an int >= 0.

If all dice categories and rolls are set, the programm will enter full automatic mode for use in other programs. This will lead to limited outputs and no ability to input new values or restart the benchmark.
