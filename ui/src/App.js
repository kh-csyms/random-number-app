import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import axios from 'axios';

function App() {
    const apiBaseUrl = process.env.REACT_APP_API_BASE_URL;
    const [number, setNumber] = useState(null);

    const fetchRandomNumber = async () => {
        try {
            const response = await axios.get('/api/random-number');
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
