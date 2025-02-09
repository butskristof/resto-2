import process from 'node:process';
import js from '@eslint/js';
import pluginVue from 'eslint-plugin-vue';
import pluginVueScopedCss from 'eslint-plugin-vue-scoped-css';
import skipFormatting from '@vue/eslint-config-prettier/skip-formatting';

export default [
  {
    name: 'app/files-to-lint',
    files: ['**/*.{vue,js,jsx,cjs,mjs}'],
  },
  {
    name: 'app/files-to-ignore',
    ignores: [
      'node_modules/**',
      'dist/**',
      'dist-ssr/**',
      'coverage/**',
      '.vscode/**',
      '.idea/**',
      '*.min.js',
      'public/**',
      'build/**',
    ],
  },
  js.configs.recommended,
  ...pluginVue.configs['flat/recommended'],
  ...pluginVueScopedCss.configs['flat/recommended'],
  skipFormatting,
  {
    languageOptions: {
      ecmaVersion: 'latest',
    },
    rules: {
      'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
      'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
      'no-restricted-imports': [
        'error',
        {
          patterns: ['../*'],
        },
      ],
      // 'vue/no-multiple-template-root': 'error',
    },
  },
];
