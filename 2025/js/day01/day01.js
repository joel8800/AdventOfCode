const fs = require('fs');

console.log('Day 01 - Advent of Code 2025');

try {
    const data = fs.readFileSync('input.txt', 'utf8').split('\\n');
    console.log('Loaded ' + data.length + ' lines');
} catch (err) {
    console.log('No input.txt found.');
}
