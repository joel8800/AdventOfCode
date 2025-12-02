const fs = require('fs');

console.log('Day 01: Secret Entrance');

const data = fs.readFileSync('input.txt', 'utf8').split('\r\n');

let position = 50;
let zerosSeen = 0;
let endAtZero = 0;

for (const line of data) 
{
    let dir = line[0] === 'L' ? -1 : 1;
    let clicks = parseInt(line.slice(1), 10);

    for (let i = 0; i < clicks; i++) {
        position = (position + dir + 100) % 100;;
        
        if (position === 0) {
            zerosSeen += 1;
        }
    }

    if (position === 0) {
        endAtZero += 1;
    }
}

console.log('Part 1: ' + endAtZero);
console.log('Part 2: ' + zerosSeen);