import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Card, Form, Button, Modal, Image } from "react-bootstrap";
import axios from "axios";
import AssignCategoryForm from "../products/assignCategoryForm";
import "../../custom.css";


const handleDeleteProductImage = (ids) => {

    axios
        .delete(`https://localhost:5001/api/Products/${ids[0]}/images/${ids[1]}`)
        .then(function (response) {
            console.log(response);
            window.location.reload(false);
        })
        .catch(function (error) {
            console.log(error);
        });
    console.log(ids)
};


export function GetProductImagesButton({ productId }) {
    const [show, setShow] = useState(false);
    const [productImages, setProductImages] = useState([]);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    useEffect(() => {
        axios
            .get(`https://localhost:5001/api/Products/${productId}/images`)
            .then((res) => setProductImages(res.data));
    }, []);
    setTimeout(() => {
        const form = document.getElementById(`createImageForm-${productId}`);
        // if (data.isFeatured) {
        //     document.getElementById("inline-readio-1").setAttribute('Checked', true);
        // } else {
        //     document.getElementById("inline-readio-2").setAttribute('Checked', true)
        // }
        if (form) {
            form.addEventListener("submit", (e) => {
                e.preventDefault();
                var formData = new FormData(form);
                axios
                    .post(`https://localhost:5001/api/Products/${productId}/images`, formData, {
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
        <>
            <Button variant="secondary" onClick={handleShow}>
                <i class="fa-solid fa-image"></i>
            </Button>

            <Modal contentClassName="wide-modal" show={show} onHide={handleClose}>
                {/* <h1>{productId}</h1> */}
                <Modal.Header closeButton>
                    <Modal.Title>Assign Categories for product Id {productId}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Id</th>
                                <th>Caption</th>
                                <th>isDefault</th>
                                <th>Sort Order</th>
                                <th>Date created</th>
                                <th></th>
                                <th></th>

                            </tr>
                            {productImages.map(image =>
                                <tr key={image.id}>
                                    <td><Image
                                        src={"https://localhost:5001/uploads/" + image.imagePath}
                                        rounded
                                        width={80}
                                        height={80}
                                    /></td>
                                    <td>{image.id}</td>
                                    <td>{image.caption}</td>
                                    <td>{image.isDefault ? "true" : "false"}</td>
                                    <td>{image.sortOrder}</td>
                                    <td>{String(new Date(image.dateCreated))}</td>
                                    <td><Button variant="danger" type="button" className="delete-product-btn" onClick={() => handleDeleteProductImage([productId, image.id])}><i class="fa-solid fa-trash"></i></Button></td>
                                    {/* <td><AssignCategoryButton productId={product.id} /><UpdateButton productId={product.id} /> </td>
                                    <td> <Button variant="danger" type="button" className="delete-product-btn" onClick={() => handleDeleteProduct(product.id)}><i class="fa-solid fa-trash"></i></Button> <GetProductImagesButton productId={product.id} /></td> */}
                                </tr>
                            )}
                        </thead>
                    </table>
                </Modal.Body>
                <Modal.Body>
                    <h2>Add Image</h2>
                    <Form action="/products/images" method="POST" encType="multipart/form-data" id={`createImageForm-${productId}`}>
                        <Row className="mb-3">
                            <Form.Group as={Col} className="mb-3">
                                <Form.Label>Caption</Form.Label>
                                <Form.Control type="text" placeholder="Enter Caption" name="Caption" />
                            </Form.Group>
                            <Form.Group as={Col} controlId="formGridEmail">
                                <Form.Label>SortOrder</Form.Label>
                                <Form.Control type="number" placeholder="Enter SortOrder" name="SortOrder" />
                            </Form.Group>
                            <Form.Group as={Col} controlId="formFile" className="mb-3" >
                                <Form.Label>File input</Form.Label>
                                <Form.Control type="file" name="ImageFile" />
                            </Form.Group>

                            <Form.Group as={Col} id="formGridCheckbox">
                                <Form.Label>Is Default</Form.Label>
                                <div key='inline-radio' className="mb-3">
                                    <Form.Check
                                        inline
                                        label="true"
                                        name="IsDefault"
                                        type="radio"
                                        id='inline-readio-images-true'
                                        value={true}
                                        defaultChecked
                                    />
                                    <Form.Check
                                        inline
                                        label="false"
                                        name="IsDefault"
                                        type="radio"
                                        id='inline-readio-images-false'
                                        value={false}
                                    />
                                </div>
                            </Form.Group>
                            <Form.Group as={Col}>

                                <Button variant="primary" type="submit">
                                    Submit
                                </Button>
                            </Form.Group>
                        </Row>
                    </Form>
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
