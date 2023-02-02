import numeral from 'numeral';
numeral.register('locale', 'nl', {
  delimiters: {
    decimal: ',',
    thousands: ' ',
  },
  currency: {
    symbol: '€',
  },
});
numeral.locale('nl');

export function formatNumber(input) {
  return numeral(input).format('0.00');
}

export function formatCurrency(input) {
  return numeral(input).format('$ 0.00');
}
