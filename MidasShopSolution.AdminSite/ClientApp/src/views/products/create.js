import { Container, Row, Col, Card, Form, Button } from "react-bootstrap";
import { useState } from "react";
import Sidebar from "../../components/Sidebar/ProductSidebar";
import axios from "axios";
function CreateForm() {
    setTimeout(() => {
        const form = document.getElementById("createForm");
        console.log("Form", form)
        if (form) {
            form.addEventListener("submit", (e) => {
                e.preventDefault();
                var formData = new FormData(form);
                formData.set("SeoAlias", formData.get('Details'))
                formData.set("SeoTitle", formData.get('Name'))
                formData.set("SeoDescription", formData.get("Details"))
                console.log(formData.get('isFeatured'))
                axios
                    .post("https://localhost:5001/api/products", formData, {
                        headers: {
                            "Content-Type": "multipart/form-data",
                        },
                    })
                    .then((res) => {
                        console.log(res);
                        window.location.reload(false);
                    })
                    .catch((err) => {
                        alert(err);
                    });
            });
        }
    }, 1000);
    return (
        <Container fluid>

            <Row className="mb-3">
                <Col xs={2} id="sidebar-wrapper">
                    <Sidebar />
                </Col>
                <Col xs={10} id="page-content-wrapper">
                    <Form action="/products/create" method="POST" encType="multipart/form-date" id="createForm">
                        <Form.Group className="mb-3">
                            <Form.Label>Name</Form.Label>
                            <Form.Control type="text" placeholder="Enter Name" name="Name" />
                        </Form.Group>
                        <Row className="mb-3">
                            <Form.Group as={Col} controlId="formGridEmail">
                                <Form.Label>Price</Form.Label>
                                <Form.Control type="number" placeholder="Enter Price" name="Price" />
                            </Form.Group>

                            <Form.Group as={Col} controlId="formGridPassword">
                                <Form.Label>OriginalPrice</Form.Label>
                                <Form.Control type="number" placeholder="Enter Original Price" name="OriginalPrice" />
                            </Form.Group>
                        </Row>
                        <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
                            <Form.Label>Example textarea</Form.Label>
                            <Form.Control as="textarea" rows={3} placeholder="Enter Description" name="Description" />
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formGridAddress1">
                            <Form.Label>Details</Form.Label>
                            <Form.Control placeholder="Enter Details" name="Details" />
                        </Form.Group>
                        <Form.Group controlId="formFile" className="mb-3" >
                            <Form.Label>Default file input example</Form.Label>
                            <Form.Control type="file" name="images" />
                        </Form.Group>
                        <Row className="mb-3">
                            <Form.Group as={Col}>
                                <Form.Label>Stock</Form.Label>
                                <Form.Control type="number" placeholder="Enter Stock" name="Stock" />
                            </Form.Group>
                            <Form.Group as={Col} id="formGridCheckbox">
                                <Form.Label>Is Featured</Form.Label>
                                <div key='inline-radio' className="mb-3">
                                    <Form.Check
                                        inline
                                        label="true"
                                        name="IsFeatured"
                                        type="radio"
                                        id='inline-readio-1'
                                        value={true}
                                    />
                                    <Form.Check
                                        inline
                                        label="false"
                                        name="IsFeatured"
                                        type="radio"
                                        id='inline-readio-2'
                                        value={false}

                                    />
                                </div>
                            </Form.Group>
                        </Row>

                        <Button variant="primary" type="submit">
                            Submit
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container >

    );
}

export default CreateForm;