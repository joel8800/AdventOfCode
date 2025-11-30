def main():
    print("Day 01 - Advent of Code 2016")
    try:
        with open('input.txt') as f:
            lines = f.read().splitlines()
            print(f"Loaded {len(lines)} lines")
    except FileNotFoundError:
        print("No input.txt found.")

if __name__ == '__main__':
    main()
