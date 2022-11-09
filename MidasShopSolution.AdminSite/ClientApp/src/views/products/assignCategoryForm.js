import { Container, Row, Col, Card, Form, Button } from "react-bootstrap";
import React, { Component } from 'react';
import Sidebar from "../../components/Sidebar/ProductSidebar";
import axios from "axios";

export default function AssignCategoryForm({ data }) {
    const productId = data[0];
    const categories = data[1];
    console.log("categories", categories)
    setTimeout(() => {
        console.log("data", productId)
        const form = document.getElementById(`assignCategoryForm-${productId}`);

        if (form) {
            form.addEventListener("submit", (e) => {
                e.preventDefault();
                var formData = new FormData(form);
                for (const value of formData.values()) {
                    console.log("a", value)
                }
                var checkedList = document.querySelectorAll('input');
                console.log("checkedList", checkedList)
                var data = {
                    categories: []
                }
                for (const item of checkedList) {
                    data.categories.push({
                        "id": item.value,
                        "selected": item.checked
                    })
                }
                console.log(data)
                axios
                    .put(`https://localhost:5001/api/Products/${productId}/categories`, data, {
                        headers: {
                            "Content-Type": "application/json-patch+json",
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
        <Form action="/products" method="PUT" encType="multipart/form-date" id={"assignCategoryForm-" + productId}>
            <div key={`default-checkbox`} className="mb-3">
                {categories.map(category =>
                    <Form.Check
                        className="categoryItem"
                        type='checkbox'
                        label={`${category.name}`}
                        id={`${category.id}`}
                        value={category.id}
                    />
                )}
            </div>
            <Button variant="primary" type="submit">
                Submit
            </Button>
        </Form>
    );
}