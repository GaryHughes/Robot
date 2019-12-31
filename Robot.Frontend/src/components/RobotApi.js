
let apiEndpoint = '';

if (process.env.REACT_APP_ROBOT_API_ENDPOINT) {
    apiEndpoint = process.env.REACT_APP_ROBOT_API_ENDPOINT;
}

export async function usage() {
    return await fetch(apiEndpoint + 'Api/1.0/Usage');
}

export async function world() {
    return await fetch(apiEndpoint + 'Api/1.0/World');
}

export async function turnLeft() {
    return await fetch(apiEndpoint + 'Api/1.0/Left', { method: 'put' });
}

export async function turnRight() {
    return await fetch(apiEndpoint + 'Api/1.0/Right', { method: 'put' });
}

export async function move() {
    return await fetch(apiEndpoint + 'Api/1.0/Move', { method: 'put' });
}

export async function place(x, y, direction) {
    return await fetch(apiEndpoint + `Api/1.0/Place/${x}/${y}/${direction}`, { method: 'put' });
}


