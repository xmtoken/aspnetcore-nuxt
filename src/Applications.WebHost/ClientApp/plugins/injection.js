export default function ({ $axios }, inject) {
  return Promise.all([
    //
    $axios.get('/enumerations').then(response => inject('enum', response.data)),
    $axios.get('/metadata').then(response => inject('metadata', response.data)),
  ]);
}
