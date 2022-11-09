import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Card, Form, Button, Modal } from "react-bootstrap";
import axios from "axios";
import AssignCategoryForm from "./assignCategoryForm";

export function AssignCategoryButton({ productId }) {
    const [show, setShow] = useState(false);
    const [categories, setCategoies] = useState([]);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    useEffect(() => {
        axios
            .get(`https://localhost:5001/api/Categories`)
            .then((res) => setCategoies(res.data));
    }, []);

    return (
        <>
            <Button variant="success" onClick={handleShow}>
                <i class="fa-solid fa-paperclip"></i>
            </Button>

            <Modal show={show} onHide={handleClose}>
                {/* <h1>{productId}</h1> */}
                <Modal.Header closeButton>
                    <Modal.Title>Assign Categories for product Id {productId}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <AssignCategoryForm data={[productId, categories]} />
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

