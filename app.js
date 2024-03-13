const express = require('express');
const http = require('http');
const fs = require('fs');
const path = require('path');
const app = express();
const axios = require('axios');
const MjpegConsumer = require('mjpeg-consumer');



let mjpegUrl = '127.0.0.1:9000';


const cors = require('cors');

// Consenti richieste cross-origin
app.use(cors({ origin: '*' }));


let n = 0;

const array = ['/R.jpeg', '/1.jpeg', '/2.jpeg'];

app.get('/scritta', (req, res) => {
    console.log("arrivato");
    res.send("ciao");

});

app.get('/immagine', (req, res) => {

    

    

    if (n == (array.length - 1)) {
        n = -1;
    }
    
    n += 1;
    console.log(n);
    const pathimg = path.join(__dirname, array[n]);
    const img = fs.readFileSync(pathimg, { encoding: 'base64' });
    res.send(img);



    
    
});

app.get('/api/stream', (req, res) => {
   
}
);




const server = http.createServer(app);
server.listen( 3000, "127.0.0.1", () => {
    console.log("Server attivo su http://127.0.0.1:3000");
});
