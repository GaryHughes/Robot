
const hostname = window && window.location && window.location.hostname;

let apiEndpoint;

//if (hostname !== 'url.com') {
    // This is the ingress IP
    //apiEndpoint = "http://35.241.14.209/";
//}
apiEndpoint = "http://localhost:8080/"

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
    console.log('HOSTNAME: ' + hostname);
    return await fetch(apiEndpoint + `Api/1.0/Place/${x}/${y}/${direction}`, { method: 'put' });
}


