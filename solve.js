const moment = require('C:/Data/node_global/node_modules/moment')


const daysOfBC = moment('19920819').dayOfYear();
const daysOfGDP = moment('19910305').dayOfYear();
const days = (daysOfBC - daysOfGDP) * 0.618 + daysOfGDP;
const date = moment('20200101').add(~~days, 'day');
console.log(date.format('YYYYMMDD'));
