
export async function turnLeft() {
    return await fetch('Api/1.0/Left', { method: 'put' });
}

export async function turnRight() {
    return await fetch('Api/1.0/Right', { method: 'put' });
}

export async function move() {
    return await fetch('Api/1.0/Move', { method: 'put' });
}

export async function place(x, y, direction) {
    return await fetch(`Api/1.0/Place/${x}/${y}/${direction}`, { method: 'put' });
}


