import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import axios from 'axios';

function App() {
    const [number, setNumber] = useState(null);

    const fetchRandomNumber = async () => {
        try {
            const response = await axios.get('http://localhost:5123/random-number'); // This was the port number on my local machine so it may need to change
            setNumber(response.data);
        } catch (error) {
            console.error('Error fetching the random number', error);
        }
    };

    return (
        <Container className="text-center mt-5">
            <Button variant="primary" onClick={fetchRandomNumber}>
                Push me for a random number
            </Button>
            {number !== null && (
                <h1 className="display-1 mt-3">{number}</h1>
            )}
        </Container>
    );
}

export default App;
