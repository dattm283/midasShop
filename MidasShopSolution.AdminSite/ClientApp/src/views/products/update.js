import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Card, Form, Button, Modal } from "react-bootstrap";
import axios from "axios";
import UpdateForm from "./formUpdate";

export function UpdateButton({ productId }) {
    const [show, setShow] = useState(false);
    const [product, setProduct] = useState({});
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    useEffect(() => {
        axios
            .get(`https://localhost:5001/api/Products/${productId}`)
            .then((res) => setProduct(res.data));
    }, []);

    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                <i class="fa-solid fa-pen-to-square"></i>
            </Button>

            <Modal show={show} onHide={handleClose}>
                {/* <h1>{productId}</h1> */}
                <Modal.Header closeButton>
                    <Modal.Title>{product.name}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <UpdateForm data={product} />
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

