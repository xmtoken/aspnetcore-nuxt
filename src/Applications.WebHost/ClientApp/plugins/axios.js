import HttpStatus from 'http-status-codes';

export default function ({ $axios, app }) {
  $axios.onRequest(config => {
    if (['delete', 'patch', 'post', 'put'].includes(config.method)) {
      config.validateStatus = status => (200 <= status && status < 300) || status === HttpStatus.BAD_REQUEST;
    }
  });

  $axios.onError(async error => {
    if (error.response.status === HttpStatus.UNAUTHORIZED || error.response.status === HttpStatus.FORBIDDEN) {
      await app.$auth.reset();
    }
  });
}
