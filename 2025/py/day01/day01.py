import sys

def main():
    print("Day 01: Secret Entrance")
    
    position = 50
    zerosSeen = 0
    endAtZero = 0
    
    with open('input.txt') as f:
        
        for line in f.read().splitlines():
            
            dir = line[0]
            clicks = int(line[1:])
            
            for _ in range(clicks):
                if dir == 'L':
                    position = (position - 1 + 100) % 100
                else:
                    position = (position + 1) % 100
                   
                if position == 0:
                    zerosSeen += 1
            
            if position == 0:
                endAtZero += 1

    print(f"Part1: {endAtZero}")
    print(f"Part2: {zerosSeen}")

if __name__ == '__main__':
    main()
