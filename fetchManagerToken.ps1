$url = "http://localhost:8888/realms/test/protocol/openid-connect/token"

$body = @{grant_type='password'
      client_id='test-client'
      username='Manager'
      password='password'}
$contentType = 'application/x-www-form-urlencoded' 
$response = Invoke-WebRequest -Method POST -Uri $url -body $body -ContentType $contentType
$responseContent = $response.Content | ConvertFrom-Json
$accessToken = $responseContent.access_token
$accessToken