const moment = require('C:/Data/node_global/node_modules/moment');

// 包辰的生日是一年中的第几天
const daysOfBC = moment('19920819').dayOfYear();
// 郭登鹏的生日是一年中的第几天
const daysOfGDP = moment('19910305').dayOfYear();
// 黄金分割点
const daysOfYear = (daysOfBC - daysOfGDP) * 0.618 + daysOfGDP;
// 第一天 + 向下取整
const date = moment('20200101').add(~~daysOfYear, 'day');
console.log(date.format('YYYYMMDD'));
