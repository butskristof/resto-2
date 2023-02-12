export const CURRENCIES = {
  EUR: new Intl.NumberFormat('nl-BE', {
    style: 'currency',
    currency: 'EUR',
  }),
  USD: new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  }),
};

export const DEFAULT_CURRENCY_INPUT_OPTIONS = {
  locale: 'nl-BE',
  currency: 'EUR',
  currencyDisplay: 'symbol',
  valueRange: {
    min: 0,
  },
  hideCurrencySymbolOnFocus: false,
  hideGroupingSeparatorOnFocus: false,
  hideNegligibleDecimalDigitsOnFocus: false,
  autoDecimalDigits: false,
  useGrouping: true,
  accountingSign: false,
};

export const DEFAULT_NUMBER_INPUT_OPTIONS = {
  locale: 'nl-NL',
  currency: 'EUR',
  currencyDisplay: 'hidden',
  hideCurrencySymbolOnFocus: true,
  hideGroupingSeparatorOnFocus: false,
  hideNegligibleDecimalDigitsOnFocus: false,
  autoDecimalDigits: false,
  useGrouping: true,
  accountingSign: false,
};
