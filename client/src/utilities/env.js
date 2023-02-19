// prefer the value from window.configs (injected in the entrypoint script)
// or fallback to the usual env vars during build
function getEnv(name) {
  return window?.configs?.[name] || import.meta.env[name];
}

export const RESTO_API_BASE_URL = getEnv('VITE_RESTO_API_BASE_URL');
