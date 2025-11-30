const fs = require('fs');

console.log('Day 02 - Advent of Code 2015');

try {
    const data = fs.readFileSync('input.txt', 'utf8').split('\\n');
    console.log('Loaded ' + data.length + ' lines');
} catch (err) {
    console.log('No input.txt found.');
}
