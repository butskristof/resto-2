import { CURRENCIES } from '@/utilities/currencies';

export function formatCurrency(amount, currency = CURRENCIES.EUR) {
  return currency.format(amount);
}

const dateTimeFormat = new Intl.DateTimeFormat('nl-BE', {
  year: 'numeric',
  month: 'numeric',
  day: 'numeric',

  hour: 'numeric',
  minute: 'numeric',
  second: 'numeric',
});

export function formatTimestamp(timestamp) {
  return dateTimeFormat.format(new Date(timestamp));
}

export function uppercase(input) {
  return input.toLocaleUpperCase();
}

export function capitalize(input) {
  return input.charAt(0).toUpperCase() + input.slice(1);
}
