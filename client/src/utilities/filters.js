const CURRENCIES = {
  EUR: new Intl.NumberFormat('nl-BE', {
    style: 'currency',
    currency: 'EUR',
  }),
  USD: new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  }),
};

export function formatCurrency(amount, currency = CURRENCIES.EUR) {
  return currency.format(amount);
}

export function uppercase(input) {
  return input.toLocaleUpperCase();
}

export function capitalize(input) {
  return input.charAt(0).toUpperCase() + input.slice(1);
}
