import { Component, OnInit } from '@angular/core';
import { UrlService } from '../../../shared/services/url.service';
import { AuthentificationService } from '../../../shared/services/authentification.service';
import { UrlModel } from '../../../shared/models/url-model';
import { Router } from '@angular/router';
import {NgForOf, NgIf} from '@angular/common';
import {FormatDatePipe} from '../../../shared/pipes/format-date.pipe';
import {SortPipe} from '../../../shared/pipes/sort-table.pipe';

@Component({
  selector: 'app-url-list',
  templateUrl: './url-table.component.html',
  imports: [
    NgForOf,
    SortPipe,
    NgIf,
  ],
  styleUrls: ['./url-table.component.scss']
})
export class UrlTableComponent implements OnInit {
  urls: UrlModel[] = [];
  paginatedUrls: UrlModel[] = [];

  sortField: string = '';
  sortOrder: 'asc' | 'desc' = 'asc';

  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalPages: number = 1;

  constructor(
    private urlService: UrlService,
    protected authentificationService: AuthentificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.urlService.getAll().subscribe(
      (data) => {
        this.urls = data;
        this.updatePagination();
      },
      (error) => {
        console.error('Failed to load urls:', error);
      }
    );
  }

  toggleSort(field: string): void {
    if (this.sortField === field) {
      this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = field;
      this.sortOrder = 'asc';
    }
  }

  getSortIcon(field: string): string {
    if (this.sortField === field) {
      return this.sortOrder === 'asc'
        ? '<i class="bi bi-sort-alpha-up"></i>'
        : '<i class="bi bi-sort-alpha-down"></i>';
    }
    return '<i class="bi bi-sort"></i>';
  }

  updatePagination(): void {
    this.totalPages = Math.ceil(this.urls.length / this.itemsPerPage);
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.paginatedUrls = this.urls.slice(startIndex, endIndex);
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePagination();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }

  editUrl(id: number): void {
    this.router.navigate([`/urls/${id}`]);
  }

  deleteUrl(id: number): void {
    this.urlService.delete(id).subscribe(
      () => {
        this.urls = this.urls.filter(url => url.id !== id);
        this.updatePagination();
      },
      (error) => {
        console.error('Failed to delete url:', error);
      }
    );
  }

  addUrl(): void {
    this.router.navigate(['/url/add']);
  }

  reloadUrls(): void {
    this.urlService.getAll().subscribe(
      (data) => {
        this.urls = data;
        this.updatePagination();
      },
      (error) => {
        console.error('Failed to reload urls:', error);
      }
    );
  }

}
