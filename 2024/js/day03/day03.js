const fs = require('fs');

console.log('Day 03 - Advent of Code 2024');

try {
    const data = fs.readFileSync('input.txt', 'utf8').split('\\n');
    console.log('Loaded ' + data.length + ' lines');
} catch (err) {
    console.log('No input.txt found.');
}
