import { CURRENCIES } from '@/utilities/currencies';

export function formatCurrency(amount, currency = CURRENCIES.EUR) {
  return currency.format(amount);
}

export function uppercase(input) {
  return input.toLocaleUpperCase();
}

export function capitalize(input) {
  return input.charAt(0).toUpperCase() + input.slice(1);
}
