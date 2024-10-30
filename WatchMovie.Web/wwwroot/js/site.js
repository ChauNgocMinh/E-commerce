async function login(email, password) {
    const response = await fetch('/User/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
    });

    if (response.ok) {
        const data = await response.json();
        localStorage.setItem('token', data.token); 
        window.location.href = '/Home/Index'; 
    } else {
        const errorData = await response.json();
        console.error('Login failed:', errorData);
    }
}
