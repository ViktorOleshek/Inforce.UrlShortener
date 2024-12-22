import { Component, Input, OnInit } from '@angular/core';
import { UrlService } from '../../../shared/services/url.service';
import { UrlModel } from '../../../shared/models/url-model';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-url-detail',
  templateUrl: './url-detail.component.html',
  imports: [
    FormsModule
  ],
  styleUrls: ['./url-detail.component.scss']
})
export class UrlDetailComponent implements OnInit {
  @Input() url: UrlModel = new UrlModel(0, '', '', new Date(), 0, '');

  constructor(
    private urlService: UrlService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadUrl(Number(id));
    }
  }

  loadUrl(id: number): void {
    this.urlService.getById(id).subscribe(
      (data) => {
        this.url = data;
      },
      (error) => {
        console.error('Error loading URL:', error);
      }
    );
  }

  saveUrl(): void {
    if (this.url.id === 0) {
      this.addUrl();
    } else {
      this.updateUrl();
    }
  }

  addUrl(): void {
    this.urlService.add(this.url).subscribe(
      (data) => {
        console.log('URL added successfully:', data);
        this.router.navigate(['/urls']);
      },
      (error) => {
        console.error('Failed to add URL:', error);
      }
    );
  }

  updateUrl(): void {
    this.urlService.update(this.url.id, this.url).subscribe(
      () => {
        console.log('URL updated successfully');
        this.router.navigate(['/urls']);
      },
      (error) => {
        console.error('Failed to update URL:', error);
      }
    );
  }

  cancel(): void {
    this.router.navigate(['/urls']);
  }
}
