import { Container, Row, Col, Card, Form, Button } from "react-bootstrap";
import React, { Component } from 'react';
import Sidebar from "../../components/Sidebar/ProductSidebar";
import axios from "axios";

export default function UpdateForm({ data }) {
    setTimeout(() => {
        console.log("data", data)
        console.log("document", document);
        const form = document.getElementById(`updateForm-${data.id}`);
        if (data.isFeatured) {
            document.getElementById("inline-readio-1").setAttribute('Checked', true);
        } else {
            document.getElementById("inline-readio-2").setAttribute('Checked', true)
        }
        console.log(`updateForm-${data.id}`, form)
        if (form) {
            form.addEventListener("submit", (e) => {
                e.preventDefault();
                const isChecked = document.getElementById("inline-readio-1");
                var formData = new FormData(form);
                console.log("formData", formData.get('Images'));
                formData.set("IsFeatured", isChecked.checked ? true : false);
                formData.set("Id", data.id)
                formData.set("SeoAlias", formData.get('Details'))
                formData.set("SeoTitle", formData.get('Name'))
                formData.set("SeoDescription", formData.get("Details"))
                // formData.set("Images", formData.get("Images") !== null ? formData.get("Images") : data.Images[0])
                console.log("isfeater", formData.get('isFeatured'))
                axios
                    .put(`https://localhost:5001/api/products`, formData, {
                        headers: {
                            "Content-Type": "multipart/form-data",
                        },
                    })
                    .then((res) => {
                        console.log("res", res);
                        window.location.reload(false);
                    })
                    .catch((err) => {
                        alert(err);
                    });
            });
        }
    }, 1000);
    return (
        <Form action="/products" method="PUT" encType="multipart/form-date" id={"updateForm-" + data.id}>
            <Form.Group className="mb-3">
                <Form.Label>Name</Form.Label>
                <Form.Control type="text" placeholder="Enter Name" name="Name" defaultValue={data.name} />
            </Form.Group>
            <Row className="mb-3">
                <Form.Group as={Col} controlId="formGridEmail">
                    <Form.Label>Price</Form.Label>
                    <Form.Control type="number" placeholder="Enter Price" name="Price" defaultValue={data.price} />
                </Form.Group>

                <Form.Group as={Col} controlId="formGridPassword">
                    <Form.Label>OriginalPrice</Form.Label>
                    <Form.Control type="number" placeholder="Enter Original Price" name="OriginalPrice" defaultValue={data.originalPrice} />
                </Form.Group>
            </Row>
            <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
                <Form.Label>Example textarea</Form.Label>
                <Form.Control as="textarea" rows={3} placeholder="Enter Description" name="Description" defaultValue={data.description} />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formGridAddress1">
                <Form.Label>Details</Form.Label>
                <Form.Control placeholder="Enter Details" name="Details" defaultValue={data.details} />
            </Form.Group>
            <Form.Group controlId="formFile" className="mb-3" >
                <Form.Label>Default file input example</Form.Label>
                <Form.Control type="file" name="images" defaultValue="" />
            </Form.Group>
            <Row className="mb-3">
                <Form.Group as={Col}>
                    <Form.Label>Stock</Form.Label>
                    <Form.Control type="number" placeholder="Enter Stock" name="Stock" defaultValue={data.stock} />
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
                            defaultValue={true}
                        // defaultChecked={data.IsFeatured}
                        />
                        <Form.Check
                            inline
                            label="false"
                            name="IsFeatured"
                            type="radio"
                            id='inline-readio-2'
                            defaultValue={false}
                        // defaultChecked={!data.IsFeatured}
                        />
                    </div>
                </Form.Group>
            </Row>

            <Button variant="primary" type="submit">
                Submit
            </Button>
        </Form>
    );
}