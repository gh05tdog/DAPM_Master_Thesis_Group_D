import http from 'k6/http';
import https from 'k6/http';
import { sleep, check } from 'k6';
export const options = {
  vus: 1,
  duration: '300s',
};

export function setup() {
  // Perform your pre-test action here
  console.log('Running setup before the test begins...');
  const host = 'https://localhost:8888'
  const realm = 'test'
  const data = {
    grant_type: 'password',
    client_id: 'test-client',
    username: 'manager',
    password: 'password',
  };
  const response = https.post(`${host}/realms/${realm}/protocol/openid-connect/token`, data);
  check(response, {
    'is status 200': (r) => r.status === 200,
    'has JWT access token': (r) => r.json().access_token.length > 0,
    'has JWT refresh token': (r) => r.json().refresh_token.length > 0,
  });
  // Return the token to be used in the main test loop
  const token = response.json('access_token');
    
  return { token }; // Return the token to be used in the default function
}
export default function (data) {
  const token = data.token;
  const headers = {
    'Authorization': `Bearer ${token}`,
  };
  
 
  const res = http.get('http://localhost:3000/users', { headers: headers });
   // Check if the resource has finished loading
   console.log(res)
   check(res, {
    'data is fully loaded': (r) => r.body.includes('status: complete'),
});
  sleep(1);
}