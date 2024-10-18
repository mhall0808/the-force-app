import React from 'react';
import { Card } from 'react-bootstrap';

interface StarshipCardProps {
    starship: {
        name: string;
        model: string;
        manufacturer: string;
        crew: string;
    };
}

// Define a type for CSS properties
const cardStyles: React.CSSProperties = {
    width: '18rem',
    margin: '1rem auto',
    textAlign: 'center' as 'center', // Ensure textAlign is a valid value
};

const imageStyles: React.CSSProperties = {
    width: '100%',
    height: 'auto',
};

const footerStyles: React.CSSProperties = {
    fontSize: '0.65rem', // Smaller text for the footer
    color: '#6c757d',    // Bootstrap's text-muted color
    marginTop: '1rem',
};

const StarshipCard: React.FC<StarshipCardProps> = ({ starship }) => {
    return (
        <Card style={cardStyles}>
            <Card.Img
                variant="top"
                src="https://res.cloudinary.com/dyb1xlxvg/image/upload/v1728795517/ComfyUI_temp_gmhel_00048__mip21q.png"
                alt="Starship"
                style={imageStyles}
            />
            <Card.Body>
                <Card.Title>{starship.name}</Card.Title>
                <Card.Footer style={footerStyles}>
                    By {starship.manufacturer}
                </Card.Footer>
            </Card.Body>
        </Card>
    );
};

export default StarshipCard;
